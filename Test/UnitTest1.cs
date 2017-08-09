using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class TestMyFirendCode
    {
        [TestMethod]
        public void TestUpdate(IItemRepository myF)
        {
            string name1 = "Name 1";
            int id = 10;
            Assert.AreEqual(id, myF.Create(id, name1)); // create an item      
            string name2 = "Name 2";
            myF.Update(id, name2);                      // update the item
            myF.Read(id);
            Assert.AreEqual(name2, myF.name);           //check updated name
            myF.Delete(id);
            Assert.IsNull(myF.Read(id));                // clean up test (delete the item)
        }

        
        [TestMethod]
        public void TestRead(IItemRepository myF)
        {
            string name1 = "Name 1";
            int id = 10;
            Assert.AreEqual(id, myF.Create(id, name1)); // create an item
            myF.Read(id);                                // read the item
            Assert.AreEqual(id, myF.id); 
            myF.Delete(id);
            Assert.IsNull(myF.Read(id));                // clean up test (delete the item)
        }



        [TestMethod]
        public void Test(IItemRepository myF)
        {

            string name1 = "Name";
            int id = 10;
            Assert.AreEqual(id, myF.Create(id, name1));
            Assert.AreEqual(name1, myF.Read(id).name);
            string name2 = "Name 2";
            myF.Update(id, name2);
            Assert.AreEqual(name2, myF.Read(id).name);
            myF.Delete(10);
            Assert.IsNull(myF.Read(id));
        }
    }
}
