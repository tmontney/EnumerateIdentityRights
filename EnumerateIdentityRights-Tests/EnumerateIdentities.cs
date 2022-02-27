﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EnumerateIdentityRights
{
    // Term 'identity' will be used to mean either a user or a group.
    [TestClass]
    public class EnumerateIdentityRights_Tests
    {
        // You are running as Administrator (or similar).
        // LsaWrapper should throw an exception.
        // (It's possible you may be able to initialize but not enumerate all identities.)
        [TestMethod]
        public void CanInitializeLsaWrapper()
        {
            try
            {
                using (EnumerateIdentityRights.LsaWrapper lsa = new EnumerateIdentityRights.LsaWrapper()) { }
            }
            catch (UnauthorizedAccessException)
            {
                Assert.Fail();
            }
        }
        // LsaEnumerateAccountRights will throw FILE_NOT_FOUND if an identity has no assigned rights.
        // GetPrivileges should handle this exception by returning an empty string array.
        // (Identity 'Administrator' should have no rights assigned out of box (inherits from identity 'Administrators'.)
        [TestMethod]
        public void NoAssignedRightsAsEmptyArray()
        {
            using (EnumerateIdentityRights.LsaWrapper lsa = new EnumerateIdentityRights.LsaWrapper())
            {
                Assert.IsTrue(lsa.GetPrivileges("Administrator").Length == 0);
            }
        }
        // LsaEnumerateAccountRights should return a non-zero count string array if an identity has rights assigned.
        // (Identity 'Administrators' should have rights assigned out of box.)
        [TestMethod]
        public void AssignedRightsAsNonEmptyArray()
        {
            using (EnumerateIdentityRights.LsaWrapper lsa = new EnumerateIdentityRights.LsaWrapper())
            {
                Assert.IsTrue(lsa.GetPrivileges("Administrators").Length != 0);
            }
        }
        // LsaEnumerateAccountRights will throw an exception if the identity cannot be found.
        // (Identity generated by System.IO.Path.GetRandomFileName almost certainly will not exist on this system.)
        [TestMethod]
        public void SIDMappingFailureAsEmptyArray()
        {
            using (EnumerateIdentityRights.LsaWrapper lsa = new EnumerateIdentityRights.LsaWrapper())
            {
                try
                {
                    lsa.GetPrivileges(System.IO.Path.GetRandomFileName());
                    Assert.Fail();
                }
                catch (Exception)
                {
                    // Do nothing
                }
            }
        }
    }
}
