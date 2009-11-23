using Parallelizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ParallelizerTest
{
    
    
    /// <summary>
    ///This is a test class for ThreadPoolCallbackerTest and is intended
    ///to contain all ThreadPoolCallbackerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ThreadPoolCallbackerTest
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
        ///A test for Queue
        ///</summary>
        [TestMethod()]
        public void QueueTest()
        {
            Action<IEnumerable<double>> callback = delegate(IEnumerable<double> results)
            {
//                MessageBox.Show(String.Join(";", results.Select(a => a.ToString()).ToArray()));
            };
            Func<int, double> func = a => Math.Sqrt((double)a);
            var target = new ThreadPoolCallbacker<int, double>(func);
            var arguments = Enumerable.Range(0, 10);
            foreach (var argument in arguments)
            {
                target.Queue(argument);
            }
            target.EnableCallback(callback);
//            MessageBox.Show(" ");
        }
    }
}
