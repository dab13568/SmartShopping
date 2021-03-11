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

        public class ItemSet
        {
            public int N; // data items are [0..N-1]
            public int k; // number of items
            public int[] data; // ex: [0 2 5]
            public int hashValue; // "0 2 5" -> 520 (for hashing)
            public int ct; // num times this occurs in transactions
            public ItemSet(int N, int[] items, int ct)
            {
                this.N = N;
                this.k = items.Length;
                this.data = new int[this.k];
                Array.Copy(items, this.data, items.Length);
                this.hashValue = ComputeHashValue(items);
                this.ct = ct;
            }
            private static int ComputeHashValue(int[] data)
            {
                int value = 0;
                int multiplier = 1;
                for (int i = 0; i < data.Length; ++i)
                {
                    value = value + (data[i] * multiplier);
                    multiplier = multiplier * 10;
                }
                return value;
            }
            public override string ToString()
            {
                string s = "{ ";
                for (int i = 0; i < data.Length; ++i)
                    s += data[i] + " ";
                return s + "}" + "   ct = " + ct; ;
            }
            public bool IsSubsetOf(int[] trans)
            {
                int foundIdx = -1;
                for (int j = 0; j < this.data.Length; ++j)
                {
                    foundIdx = IndexOf(trans, this.data[j], foundIdx + 1);
                    if (foundIdx == -1) return false;
                }
                return true;
            }
            private static int IndexOf(int[] array, int item, int startIdx)
            {
                for (int i = startIdx; i < array.Length; ++i)
                {
                    if (i > item) return -1; // i is past target loc
                    if (array[i] == item) return i;
                }
                return -1;
            }
        }
        public List<int[]> GetFrequentItemSets(int N, List<int[]> transactions,
double minSupportPct, int minItemSetLength, int maxItemSetLength)
        {
            int minSupportCount = (int)(transactions.Count * minSupportPct);
            Dictionary<int, bool> frequentDict = new Dictionary<int, bool>();
            List<ItemSet> frequentList = new List<ItemSet>();
            List<int> validItems = new List<int>();
            int[] counts = new int[N];
            for (int i = 0; i < transactions.Count; ++i)
            {
                for (int j = 0; j < transactions[i].Length; ++j)
                {
                    int v = transactions[i][j];
                    ++counts[v];
                }
            }
            for (int i = 0; i < counts.Length; ++i)
            {
                if (counts[i] >= minSupportCount)
                {
                    validItems.Add(i); // i is the item-value
                    int[] d = new int[1]; // ItemSet ctor wants an array
                    d[0] = i;
                    ItemSet ci = new ItemSet(N, d, 1); // size 1, ct 1
                    frequentList.Add(ci); // it's frequent
                    frequentDict.Add(ci.hashValue, true); // record
                } // else skip this item
            }
            bool done = false;
            for (int k = 2; k <= maxItemSetLength && done == false; ++k)
            {
                done = true;
                int numFreq = frequentList.Count;
                for (int i = 0; i < numFreq; ++i)
                {
                    if (frequentList[i].k != k - 1) continue;
                    for (int j = 0; j < validItems.Count; ++j)
                    {
                        int[] newData = new int[k]; // data for a candidate item-set
                        for (int p = 0; p < k - 1; ++p)
                            newData[p] = frequentList[i].data[p];
                        if (validItems[j] <= newData[k - 2]) continue;
                        newData[k - 1] = validItems[j];
                        ItemSet ci = new ItemSet(N, newData, -1);
                        if (frequentDict.ContainsKey(ci.hashValue) == true)
                            continue;
                        int ct = CountTimesInTransactions(ci, transactions);
                        if (ct >= minSupportCount)
                        {
                            ci.ct = ct;
                            frequentList.Add(ci);
                            frequentDict.Add(ci.hashValue, true);
                            done = false;
                        }
                    } // j
                } // i
                validItems.Clear();
                Dictionary<int, bool> validDict = new Dictionary<int, bool>();
                for (int idx = 0; idx < frequentList.Count; ++idx)
                {
                    if (frequentList[idx].k != k) continue;
                    for (int j = 0; j < frequentList[idx].data.Length; ++j)
                    {
                        int v = frequentList[idx].data[j]; // item
                        if (validDict.ContainsKey(v) == false)
                        {
                            validItems.Add(v);
                            validDict.Add(v, true);
                        }
                    }
                }
                validItems.Sort();
            } // next k
            List<int[]> result = new List<int[]>();
            for (int i = 0; i < frequentList.Count; ++i)
            {
                if (frequentList[i].k >= minItemSetLength)
                    result.Add(
                      frequentList[i].data);
            }
            return result;
        }
        public int CountTimesInTransactions(ItemSet itemSet,
  List<int[]> transactions)
        {
            int ct = 0;
            for (int i = 0; i < transactions.Count; ++i)
            {
                if (itemSet.IsSubsetOf(transactions[i]) == true)
                    ++ct;
            }
            return ct;
        }

        public List<Product> getReccomendationByDay(string dt)
        {

            var collectionOfSets = rep.getAllpurchasesIdsForDayReccomendation();
            List<Product> products = new List<Product>();
            List<int[]> productsIdSetsRec = GetFrequentItemSets(rep.getSizeOfProducts()+1, collectionOfSets, 0.4,2, 4);
            int cnt = 0;
            foreach (var ProductsIdSet in productsIdSetsRec)
            {
                cnt++;
                var kuku = ProductsIdSet.First<int>();
                //Product firstP=rep.getProductById(kuku);
                if (ProductsIdSet.All(value => rep.isInScannedProductInDay(value, dt)))
                {
                    for (int i = 0; i < ProductsIdSet.Length; i++)
                    {
                        
                        Product p = rep.getProductById(ProductsIdSet[i]);
                        if (!products.Any(value => value.num == p.num))
                            products.Add(p);
                    }
                }
            }
            //friends.Remove(product);
            return products;
        }

        public int GetLongestTransaction(List<int[]> collectionOfSets)
        {
            int maxSize=0;
            foreach( var set in collectionOfSets)
            {
                if (set.Count() > maxSize)
                    maxSize = set.Count();
            }
            return maxSize;
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
