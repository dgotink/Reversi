using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reversi.Cells;

namespace ReversiLibTests
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void Linq_SingleDependency()
        {
            var x = Cell.Create( 5 );
            var y = from val in x
                    select val * val;

            Assert.AreEqual( 25, y.Value );
            x.Value++;
            Assert.AreEqual( 36, y.Value );
        }

        [TestMethod]
        public void Linq_TwoDependencies()
        {
            var x = Cell.Create( 3 );
            var y = Cell.Create( 4 );
            var z = from a in x
                    from b in y
                    select a * b;

            Assert.AreEqual( 3 * 4, z.Value );
            x.Value++;
            Assert.AreEqual( 4 * 4, z.Value );
            y.Value++;
            Assert.AreEqual( 4 * 5, z.Value );
        }

        [TestMethod]
        public void Linq_ThreeDependencies()
        {
            var x = Cell.Create( 3 );
            var y = Cell.Create( 4 );
            var z = Cell.Create( 5 );
            var r = from a in x
                    from b in y
                    from c in z
                    select a * b * c;

            Assert.AreEqual( 3 * 4 * 5, r.Value );
            x.Value++;
            Assert.AreEqual( 4 * 4  * 5, r.Value );
            y.Value++;
            Assert.AreEqual( 4 * 5 * 5 , r.Value );
            z.Value++;
            Assert.AreEqual( 4 * 5 * 6, r.Value );
        }
    }
}
