using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    public class FuzzyLogicTest
    {

        private readonly ITestOutputHelper output;

        public FuzzyLogicTest(ITestOutputHelper output)
        {
            this.output = output;
            //output.WriteLine(...);
        }

        [Fact]
        public void CreateFuzzyModule()
        {
            FuzzyModule fuzzyModule = new FuzzyModule();
            Assert.NotEqual(null, fuzzyModule);
        }

        [Fact]
        public void CreateFuzzyFLV()
        {
            FuzzyModule fuzzyModule = new FuzzyModule();

            FuzzyVariable distanceToTarget = fuzzyModule.CreateFLV("DistanceToTarget");

            Assert.NotEqual(null, distanceToTarget);
        }

        [Fact]
        public void FuzzyLogic_Test1()
        {
            FuzzyModule fuzzyModule = new FuzzyModule();

            FuzzyVariable distanceToTarget = fuzzyModule.CreateFLV("DistanceToTarget");
            FzSet Target_Close = distanceToTarget.AddLeftShoulderSet("Target_Close", 0, 250, 500);
            FzSet Target_Medium = distanceToTarget.AddTriangularSet("Target_Medium", 250, 500, 750);
            FzSet Target_Far = distanceToTarget.AddRightShoulderSet("Target_Far", 500, 750, 1000);

            FuzzyVariable AmmoStatus = fuzzyModule.CreateFLV("AmmoStatus");
            FzSet Ammo_Low = AmmoStatus.AddLeftShoulderSet("Ammo_Low", 0, 250, 500);
            FzSet Ammo_Okay = AmmoStatus.AddTriangularSet("Ammo_Okay", 250, 500, 750);
            FzSet Ammo_Loads = AmmoStatus.AddRightShoulderSet("Ammo_Loads", 500, 750, 1000);

            FuzzyVariable Desirability = fuzzyModule.CreateFLV("Desirability");
            FzSet Undesirable = Desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);
            FzSet Desirable = Desirability.AddTriangularSet("Desirable", 25, 50, 75);
            FzSet Very_Desirable = Desirability.AddRightShoulderSet("Very_Desirable", 50, 75, 100);

            fuzzyModule.AddRule(new FzAND(Target_Close, Ammo_Loads), Undesirable);
            fuzzyModule.AddRule(new FzAND(Target_Close, Ammo_Okay), Undesirable);
            fuzzyModule.AddRule(new FzAND(Target_Close, Ammo_Low), Undesirable);
            fuzzyModule.AddRule(new FzAND(Target_Medium, Ammo_Loads), Very_Desirable);
            fuzzyModule.AddRule(new FzAND(Target_Medium, Ammo_Okay), Very_Desirable);
            fuzzyModule.AddRule(new FzAND(Target_Medium, Ammo_Low), Desirable);
            fuzzyModule.AddRule(new FzAND(Target_Far, Ammo_Loads), Desirable);
            fuzzyModule.AddRule(new FzAND(Target_Far, Ammo_Okay), Desirable);
            fuzzyModule.AddRule(new FzAND(Target_Far, Ammo_Low), Undesirable);

            fuzzyModule.Fuzzify("DistanceToTarget", 200);
            fuzzyModule.Fuzzify("AmmoStatus", 400);

            double value = fuzzyModule.DeFuzzify("Desirability", FuzzyModule.DefuzzifyMethod.MaxAV);

            Assert.Equal(12.5, value);

            fuzzyModule.Fuzzify("DistanceToTarget", 1000);
            fuzzyModule.Fuzzify("AmmoStatus", 600);

            value = fuzzyModule.DeFuzzify("Desirability", FuzzyModule.DefuzzifyMethod.MaxAV);

            Assert.Equal(50, value);
        }
    }
}
