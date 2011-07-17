using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SadPanda.Test.ConsoleApplication
{

    public static class MyExtentionMethods
    {
        public static void DisplayFighters(this IEnumerable leagueParam)
        {
            foreach (Fighter f in leagueParam)
            {
                Console.WriteLine(f.Name + 'f');
                //+f.Name+' Att:'+f.AttackStrength+' Def:'+f.DefenceStrength+' Life:'+f.Life+' Luck:'+f.Luck+' Strength:'+f.Strength); 
                //Name + ' ' + f.Luck + ' ' + f.Strength + ' ' + f.Name + ' ' + f.Name + ' ' + f.Name);
            }
        }
        public static IEnumerable<Fighter> Filter(
            this League League,
            Func<Fighter, bool> selectorParam)
        {
            foreach (Fighter f in League.Fighters)
            {
                if (selectorParam(f))
                {
                    yield return f;
                }
            }
        }
        public static void DisplayAttacks(this IEnumerable<Attack> attackParam)
        {
            foreach (Attack a in attackParam)
            {
                Console.WriteLine(a.Name + 'a');
            }
        }
    }

    public class Attack : IEnumerable
    {
        public string Name { get; set; }
        public IEnumerator GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class League : IEnumerable
    {
        public string Name { get; set; }
        public List<Fighter> Fighters { get; set; }
        public IEnumerator GetEnumerator()
        {
            return Fighters.GetEnumerator();
        }
    }
    public class Fighter
    {
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Life { get; set; }
        public int AttackStrength { get; set; }
        public int DefenceStrength { get; set; }
        public int Luck { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            League UFC = new League
            {
                Fighters = new List<Fighter>{
                new Fighter { Name = "Brock", AttackStrength = 85, DefenceStrength=60, Life=70, Luck = 50, Strength= 98 },
                new Fighter { Name = "George", AttackStrength = 75, DefenceStrength=85, Life=85, Luck = 60, Strength= 88 },
                new Fighter { Name = "Big Counry", AttackStrength = 55, DefenceStrength=87, Life=95, Luck = 80, Strength= 78 },
                new Fighter { Name = "Del Santos", AttackStrength = 80, DefenceStrength=75, Life=75, Luck = 70, Strength= 78 } 
                }
            };

            //UFC.DisplayFighters();
            List<Fighter> Fighters = new List<Fighter>{
                new Fighter { Name = "Brock", AttackStrength = 85, DefenceStrength=60, Life=70, Luck = 50, Strength= 98 },
                new Fighter { Name = "George", AttackStrength = 75, DefenceStrength=85, Life=85, Luck = 60, Strength= 88 },
                new Fighter { Name = "Big Counry", AttackStrength = 55, DefenceStrength=87, Life=95, Luck = 80, Strength= 78 },
                new Fighter { Name = "Del Santos", AttackStrength = 80, DefenceStrength=75, Life=75, Luck = 70, Strength= 78 } 
            };

            Func<Fighter, bool> DefenceStrengthFilter = f => f.DefenceStrength > 85;
            IEnumerable<Fighter> BigDefenderFighters = UFC.Filter(DefenceStrengthFilter);
        }
    }
}
