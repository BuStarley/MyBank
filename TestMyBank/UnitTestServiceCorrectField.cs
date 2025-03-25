using MyBank;

namespace TestMyBank
{
    [TestClass]
    public class UnitTestServiceCorrectField
    {
        [TestMethod]
        public void IsGender_Null()
        {
            var serviceCorrectField = new ServiceCorrectField();
            bool excepted = false;

            bool actual = serviceCorrectField.IsGender(null);

            Assert.AreEqual(excepted, actual);
        }

        [TestMethod]
        public void IsGender_Empty()
        {
            var serviceCorrectField = new ServiceCorrectField();
            bool excepted = false;

            bool actual = serviceCorrectField.IsGender("");

            Assert.AreEqual(excepted, actual);
        }

        [TestMethod]
        public void IsGender_Value()
        {
            var serviceCorrectField = new ServiceCorrectField();
            string value = "value";
            bool excepted = true;

            bool actual = serviceCorrectField.IsGender(value);

            Assert.AreEqual(excepted, actual);
        }
    }
}