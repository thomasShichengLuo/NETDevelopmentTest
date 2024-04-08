using Booster.CodingTest.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETDevelopmentTest
{
    public interface InterfaceTextProcesser
    {
        public Task ProcessStream(Stream stream);
        public void DisplayResults();
    }
}
