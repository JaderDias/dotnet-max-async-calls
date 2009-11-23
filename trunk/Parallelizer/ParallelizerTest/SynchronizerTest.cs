using Parallelizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace ParallelizerTest
{
    
    
    /// <summary>
    ///This is a test class for SynchronizerTest and is intended
    ///to contain all SynchronizerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SynchronizerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for Execute
        ///</summary>
        [TestMethod()]
        public void ExecuteTest()
        {
            var target = new AsyncCallbackSynchronizer<object>();
            var request = HttpWebRequest.Create("http://www.google.com");
            IAsyncResult actual = target.Execute(request.BeginGetResponse, null);
        }

        /// <summary>
        ///A test for Execute
        ///</summary>
        [TestMethod()]
        public void ExecuteTest1()
        {
            var target = new Synchronizer<IEnumerable<double>>();
            Func<int, double> func = a => Math.Sqrt((double)a);
            var callbacker = new ThreadPoolCallbacker<int, double>(func);
            var arguments = Enumerable.Range(0, 10);
            foreach (var argument in arguments)
            {
                callbacker.Queue(argument);
            }
            var result = target.Execute(callbacker.EnableCallback)
                .ToArray();
        }

        [TestMethod()]
        public void ExecuteTest2()
        {
            var urls = new string[]
            {
                "http://www.google.com.br/",
                "http://www.microsoft.com/"
            };
            urls = urls.Concat(urls).Concat(urls).Concat(urls).Concat(urls).Concat(urls).Concat(urls).Concat(urls).ToArray();
            var asyncCallbackSynchronizer = new AsyncCallbackSynchronizer<string>();
            Func<AsyncCallback, string, IAsyncResult> begin = delegate(AsyncCallback callback, string url)
            {
                var request = HttpWebRequest.Create(url);
                return request.BeginGetResponse(callback, request);
            };
            var synchronizer = new Synchronizer<IEnumerable<IAsyncResult>>();
            var callbacker = new ThreadPoolCallbacker<SynchronizationDetails<string>, IAsyncResult>(SynchronizationDetails<string>.Execute);
            foreach (var url in urls)
            {
                var synchronizationDetails = new SynchronizationDetails<string>
                {
                    Argument = url,
                    Begin = begin,
                    AsyncCallbackSynchronizer = asyncCallbackSynchronizer
                };
                callbacker.Queue(synchronizationDetails);
            }
            var results = synchronizer.Execute(callbacker.EnableCallback);
            var reals = new List<string>();
            foreach (var result in results)
            {
                using (var response = ((WebRequest)result.AsyncState).EndGetResponse(result))
                {
                    reals.Add(response.ResponseUri.AbsoluteUri);
                }
            }
        }
    }
}
