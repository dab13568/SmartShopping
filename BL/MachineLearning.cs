using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace BL
{
    public class MachineLearning
    {

        //private  string dataPath = Path.Combine(Environment.CurrentDirectory, "amazon0302.txt");
        DAL.Repository rep;

        public MachineLearning()
        { //dataPath = Path.Combine(Environment.CurrentDirectory, "amazon0302.txt");
            rep = new DAL.Repository();
            
        }

        public class ProductCoupleVector
        {
            [LoadColumn(0)] public int ProductID { get; set; }
            [LoadColumn(1)] public int CombinedProductID { get; set; }

        }
        public class ProductCouple
        {
            [LoadColumn(0)] public float ProductID { get; set; }
            [LoadColumn(1)] public float CombinedProductID { get; set; }
            public ProductCouple() { ProductID = 1; CombinedProductID = 2; }
        }

        /// <summary>
        /// The ProductPrediction class represents a prediction made by the model.
        /// </summary>
        public class ProductPrediction
        {
            public float Score { get; set; }
        }
        ///// <summary>
        ///// The main entry point of the program.
        ///// </summary>
        ///// <param name="args">The command line arguments.</param>
        //public void execute()
        //{

        //    // create a machine learning context
        //    var context = new MLContext();

        //    //int featureDimension = 2;
        //    //var definedSchema = SchemaDefinition.Create(typeof(ProductCouple));
        //    //var featureColumn = definedSchema[0]
        //    //    .ColumnType as VectorDataViewType;

        //    //var vectorItemType = (VectorDataViewType)Int32;
        //    //definedSchema[0].ColumnType = int;

        //    IDataView data = context.Data
        //        .LoadFromEnumerable<ProductCouple>(new List<ProductCouple> { new ProductCouple(), new ProductCouple(), new ProductCouple(), new ProductCouple() });

        //    // load the dataset in memory
        //    Console.WriteLine("Loading data...");
        //    //var data = context.Data.LoadFromEnumerable<ProductInfo>(
        //    //    dataPath,
        //    //    hasHeader: true,
        //    //    separatorChar: '\t');

        //    // split the data into 80% training and 20% testing partitions
        //    var partitions = context.Data.TrainTestSplit(data, testFraction: 0.2);
        //    var options = new MatrixFactorizationTrainer.Options()
        //    {
        //        MatrixColumnIndexColumnName = "ProductIDEncoded",
        //        MatrixRowIndexColumnName = "CombinedProductIDEncoded",
        //        LabelColumnName = "CombinedProductID",
        //        LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass,
        //        Alpha = 0.01,
        //        Lambda = 0.025,
        //    };

        //    // set up a training pipeline
        //    // step 1: map ProductID and CombinedProductID to keys
        //    var pipeline = context.Transforms.Conversion.MapValueToKey(
        //            inputColumnName: "ProductID",
        //            outputColumnName: "ProductIDEncoded")
        //        .Append(context.Transforms.Conversion.MapValueToKey(
        //            inputColumnName: "CombinedProductID",
        //            outputColumnName: "CombinedProductIDEncoded"))

        //        // step 2: find recommendations using matrix factorization
        //        .Append(context.Recommendation().Trainers.MatrixFactorization(options));

        //    // train the model
        //    Console.WriteLine("Training the model...");
        //    ITransformer model = pipeline.Fit(partitions.TrainSet);
        //    Console.WriteLine();
        //    Console.WriteLine("Evaluating the model...");
        //    var predictions = model.Transform(partitions.TestSet);
        //    //var metrics = context.Regression.Evaluate(predictions, labelColumnName: "CombinedProductID", scoreColumnName: "DefaultColumnNames.Score");
        //    //Console.WriteLine($"  RMSE: {metrics.RootMeanSquaredError:#.##}");
        //    //Console.WriteLine($"  L1:   {metrics.MeanAbsoluteError:#.##}");
        //    //Console.WriteLine($"  L2:   {metrics.MeanSquaredError:#.##}");
        //    Console.WriteLine();
        //    // check how well products 3 and 63 go together
        //    Console.WriteLine("Predicting if two products combine...");
        //    var engine = context.Model.CreatePredictionEngine<ProductCouple, ProductPrediction>(model);
        //    var prediction = engine.Predict(
        //        new ProductCouple()
        //        {
        //            ProductID = 3,
        //            CombinedProductID = 63
        //        });
        //    Console.WriteLine($"  Score of products 3 and 63 combined: {prediction.Score}");
        //    Console.WriteLine();

        //    // the rest of the code goes here....


        //    Console.WriteLine("Calculating the top 5 products for product 3...");
        //    var top5 = (from m in Enumerable.Range(1, 262111)
        //                let p = engine.Predict(
        //                   new ProductCouple()
        //                   {
        //                       ProductID = 3,
        //                       CombinedProductID = (int)m
        //                   })
        //                orderby p.Score descending
        //                select (ProductID: m, Score: p.Score)).Take(5);
        //    foreach (var t in top5)
        //        Console.WriteLine($"  Score:{t.Score}\tProduct: {t.ProductID}");
        //}
        public List<ProductCouple> initiallize()
        {
            List<ProductCouple> result = new List<ProductCouple>();
            
            var list = rep.getCouplesOfProductForProductRecommendation();
            foreach (var element in list)
            {
                ProductCouple couple = new ProductCouple();
                couple.ProductID = element.Item1;
                couple.CombinedProductID = element.Item2;
                result.Add(couple);
            }
            return result;

        }


        public Dictionary<Product,float> execute(int id)
        {
            var context = new MLContext();

            //int featureDimension = 2;
            //var definedSchema = SchemaDefinition.Create(typeof(ProductCouple));
            //var featureColumn = definedSchema[0]
            //    .ColumnType as VectorDataViewType;

            //var vectorItemType = (VectorDataViewType)Int32;
            //definedSchema[0].ColumnType = int;
            var dataset = initiallize();
            IDataView data = context.Data
                .LoadFromEnumerable<ProductCouple>(dataset);

            // load the dataset in memory
            Console.WriteLine("Loading data...");
            //var data = context.Data.LoadFromEnumerable<ProductInfo>(
            //    dataPath,
            //    hasHeader: true,
            //    separatorChar: '\t');

            // split the data into 80% training and 20% testing partitions
            var partitions = context.Data.TrainTestSplit(data, testFraction: 0.1);
            var options = new MatrixFactorizationTrainer.Options()
            {
                MatrixColumnIndexColumnName = "ProductIDEncoded",
                MatrixRowIndexColumnName = "CombinedProductIDEncoded",
                LabelColumnName = "CombinedProductID",
                LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass,
                Alpha = 0.01,
                Lambda = 0.025,
            };

            // set up a training pipeline
            // step 1: map ProductID and CombinedProductID to keys
            var pipeline = context.Transforms.Conversion.MapValueToKey(
                    inputColumnName: "ProductID",
                    outputColumnName: "ProductIDEncoded")
                .Append(context.Transforms.Conversion.MapValueToKey(
                    inputColumnName: "CombinedProductID",
                    outputColumnName: "CombinedProductIDEncoded"))

                // step 2: find recommendations using matrix factorization
                .Append(context.Recommendation().Trainers.MatrixFactorization(options));

            // train the model
            Console.WriteLine("Training the model...");
            ITransformer model = pipeline.Fit(partitions.TrainSet);
            Console.WriteLine();
            Console.WriteLine("Evaluating the model...");
            var predictions = model.Transform(partitions.TestSet);
            //var metrics = context.Regression.Evaluate(predictions, labelColumnName: "CombinedProductID", scoreColumnName: "DefaultColumnNames.Score");
            //Console.WriteLine($"  RMSE: {metrics.RootMeanSquaredError:#.##}");
            //Console.WriteLine($"  L1:   {metrics.MeanAbsoluteError:#.##}");
            //Console.WriteLine($"  L2:   {metrics.MeanSquaredError:#.##}");
            Console.WriteLine();
            // check how well products 3 and 63 go together
            Console.WriteLine("Predicting if two products combine...");
            var engine = context.Model.CreatePredictionEngine<ProductCouple, ProductPrediction>(model);
            var prediction = engine.Predict(
                new ProductCouple()
                {
                    ProductID = 125,
                    CombinedProductID = 129
                });
            Console.WriteLine($"  Score of products 3 and 63 combined: {prediction.Score}");
            Console.WriteLine();

            // the rest of the code goes here....


            Console.WriteLine("Calculating the top 5 products for product 3...");
            var top5 = (from m in Enumerable.Range(1, 1000)
                        let p = engine.Predict(
                           new ProductCouple()
                           {
                               ProductID = id,
                               CombinedProductID = (uint)m
                           })
                        orderby p.Score descending
                        select (ProductID: m, Score: p.Score)).Take(6);
            var top5List = top5.ToList().FindAll(value=> value.ProductID!=id);
            
            Dictionary<Product, float> dict = new Dictionary<Product, float>();
            foreach (var t in top5List)
            {
                if (t.Score.Equals(float.NaN))
                    break;
                dict.Add(rep.getProductById(t.ProductID), t.Score);
            }


            return dict;




        }

        //private bool equalId((int ProductID, float Score) obj)
        //{
        //    return obj.ProductID == id;
        //}

        

    }
}
