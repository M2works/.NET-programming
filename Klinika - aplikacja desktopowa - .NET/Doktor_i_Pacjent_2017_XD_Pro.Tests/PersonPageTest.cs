// <copyright file="PersonPageTest.cs">Copyright ©  2017</copyright>
using System;
using Doktor_i_Pacjent_2017_XD_Pro.DataLogic;
using Doktor_i_Pacjent_2017_XD_Pro.FrontWPF;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doktor_i_Pacjent_2017_XD_Pro.FrontWPF.Tests
{
    /// <summary>This class contains parameterized unit tests for PersonPage</summary>
    [PexClass(typeof(PersonPage))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class PersonPageTest
    {
        /// <summary>Test stub for .ctor(Logic, Patient)</summary>
        [PexMethod]
        public PersonPage ConstructorTest(Logic l, Patient p)
        {
            PersonPage target = new PersonPage(l, p);
            return target;
            // TODO: add assertions to method PersonPageTest.ConstructorTest(Logic, Patient)
        }
    }
}
