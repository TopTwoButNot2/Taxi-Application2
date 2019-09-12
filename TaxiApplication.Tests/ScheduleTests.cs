using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TaxiApplication.BusinessLogics;
using TaxiApplication.Controllers;
using TaxiApplication.Data;

namespace TaxiApplication.Tests
{
    [TestFixture]
    public class ScheduleTests
    {
        public Schedule GetSchedule(int? id)
        {
            if (id == 0)
                return null;
            if (id == null)
                throw new InvalidOperationException();
            var schedule =  new Schedule
            {
                id = 1,
                No =2,
                Position = 1,
                Routeee = "Mount Edgcombe"
            };
            return schedule;
        }

        [Test]
        public void GetSchedule_WhenIdEqualsOne_ReturnSchedule()
        {
            var result = GetSchedule(1);
             
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetSchedule_WhenIdEqualsZero_ReturnNull()
        {
            var result = GetSchedule(0);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetSchedule_WhenIdIsNull_ReturnInvalidOperationException()
        {
            
            Assert.That(()=> GetSchedule(null), Throws.InvalidOperationException);
        }
    }
}
