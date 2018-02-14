using NUnit.Framework;
using DIKUArcade.Entities;

namespace DIKUArcadeUnitTests.Entities {
    [TestFixture]
    public class TestIEnumerableInterface {

        [Test]
        public void TestIterationUnchanged() {
            var ent0 = new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f);
            var ent1 = new StationaryShape(1.0f, 1.0f, 1.0f, 1.0f);
            var ent2 = new StationaryShape(2.0f, 2.0f, 2.0f, 2.0f);
            var ent3 = new StationaryShape(3.0f, 3.0f, 3.0f, 3.0f);
            var ent4 = new StationaryShape(4.0f, 4.0f, 4.0f, 4.0f);
            var ent5 = new StationaryShape(5.0f, 5.0f, 5.0f, 5.0f);
            var ents = new EntityContainer();

            foreach (var shp in new[] {ent0, ent1, ent2, ent3, ent4, ent5}) {
                ents.AddStationaryEntity(shp, null);
            }

            foreach (Entity ent in ents) {
                ent.Shape.Position.X *= -1.0f;
                ent.Shape.Position.Y *= -1.0f;
            }

            foreach (Entity ent in ents) {
                // TODO: Ask Boris for advice on safe pointers in ReadOnlyCollections
                //Assert.GreaterOrEqual(ent.Shape.Position.X, 0.0f);
                //Assert.GreaterOrEqual(ent.Shape.Position.Y, 0.0f);
            }
        }

        [Test]
        public void TestIterationChanged() {
            var ent0 = new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f);
            var ent1 = new StationaryShape(1.0f, 1.0f, 1.0f, 1.0f);
            var ent2 = new StationaryShape(2.0f, 2.0f, 2.0f, 2.0f);
            var ent3 = new StationaryShape(3.0f, 3.0f, 3.0f, 3.0f);
            var ent4 = new StationaryShape(4.0f, 4.0f, 4.0f, 4.0f);
            var ent5 = new StationaryShape(5.0f, 5.0f, 5.0f, 5.0f);
            var ents = new EntityContainer();

            foreach (var shp in new[] {ent0, ent1, ent2, ent3, ent4, ent5}) {
                ents.AddStationaryEntity(shp, null);
            }

            ents.Iterate(entity => {
                entity.Shape.Position.X *= -1.0f;
                entity.Shape.Position.Y *= -1.0f;
            });

            foreach (Entity ent in ents) {
                Assert.LessOrEqual(ent.Shape.Position.X, 0.0f);
                Assert.LessOrEqual(ent.Shape.Position.Y, 0.0f);
            }
        }
    }
}