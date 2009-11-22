using Parallelizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Threading;

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
            var target = new Synchronizer<object>();
            var request = HttpWebRequest.Create("http://www.google.com");
            IAsyncResult actual = target.Execute(request.BeginGetResponse, null);
        }

        double _result;

        public void UnencapsulatedQueueing()
        {
            var autoResetEvent = new AutoResetEvent(false);
            WaitCallback method = delegate(object state)
            {
                _result = Math.Sqrt((double)state);
                autoResetEvent.Set();
            };

            ThreadPool.QueueUserWorkItem(method, 4d);
            autoResetEvent.WaitOne();
        }

        /// <summary>
        ///A test for Execute
        ///</summary>
        [TestMethod()]
        public void ExecuteTest1()
        {
            var target = new Synchronizer<object>();
            var request = HttpWebRequest.Create("http://www.google.com");
            IAsyncResult actual = target.Execute(request.BeginGetResponse, null);
        }

        [TestMethod()]
        public void ExecuteTest2()
        {
            //var urls = new string[]
            //{
            //    "http://google.com",
            //    "http://stackoverflow.com"
            //};
            //foreach (var url in urls)
            //{
            //    var request = HttpWebRequest.Create(url);
            //    ThreadPool.QueueUserWorkItem(delegate(object arg)
            //    {
            //        request.BeginGetResponse(delegate(IAsyncResult ar)
            //        {

            //        }, null
            //    }, null);


            //    var synchronizer = new Synchronizer<object>();
            //    synchronizer.Execute(ThreadPool.QueueUserWorkItem(

                //var synchronizer = new Synchronizer<object>();
                //var result = synchronizer.Execute(request.BeginGetResponse, null);
                //using (var response = request.EndGetResponse(result))
                //{
                //}
            //}
        }
    }
}
