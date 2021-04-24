using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dictator.Core
{
    public class AudienceStats : IAudienceStats
    {
        private Audience[] audiences;

        public AudienceStats()
        {
            Initialise();
        }

        public void Initialise()
        {
            audiences = new Audience[] {
                new Audience(GroupType.Army, 0, -5/*H*/, "QJLMMMMM", "PKLMMM", "     INTRODUCE CONSCRIPTION     "),
                new Audience(GroupType.Army, 0, 0, "PMJMMMMM", "NMLMMM", " REQUISITION LAND for TRAINING  "),
                new Audience(GroupType.Army, -10/*C*/, 0, "PLNMLMLM", "NMNIMM", "   ATTACK ALL GUERILLA BASES    "),
                new Audience(GroupType.Army, -8/*E*/, 0, "PLMMIMLM", "NMNKMM", "ATTACK GUERILLA BASES in LEFTOTO"),
                new Audience(GroupType.Army, 0, 0, "QONMMIMM", "NMNMMJ", "  SACK the SECRET POLICE CHIEF  "),
                new Audience(GroupType.Army, 0, 0, "PMMMLMIO", "MMMMMM", "EXPEL RUSSIAN MILITARY ADVISORS "),
                new Audience(GroupType.Army, 0, -9/*D*/, "QMLMMMMM", "OLLLMM", " INCREASE the PAY of the TROOPS "),
                new Audience(GroupType.Army, -12/*A*/, 0, "QLLMLLMM", "PLLKLM", "  BUY MORE ARMS and AMMUNITION  "),
                new Audience(GroupType.Peasants, 0, 3/*P*/, "LONMMMMM", "LMMLMM", "   STOP ARMY SIGN-UP COERCION   "),
                new Audience(GroupType.Peasants, 0, 0, "MQIMNMMM", "MOLMMM", "INCREASE the BASIC MINIMUM WAGE "),
                new Audience(GroupType.Peasants, 0, 0, "NQOMMIMM", "NNNNMJ", " CUT the POWERS of the S.POLICE "),
                new Audience(GroupType.Peasants, 0, 0, "MPKMKMMM", "MOKMMM", "STOP LEFTOTAN IMMIGRANT WORKERS "),
                new Audience(GroupType.Peasants, -10/*C*/, -8/*E*/, "LQKMOLNM", "MNLLMM", "INTRODUCE FREE EDUCATION for ALL"),
                new Audience(GroupType.Peasants, 0, 0, "MQJMNLNM", "MPJMML", "LEGALISE the FORMATION of UNIONS"),
                new Audience(GroupType.Peasants, 0, 0, "LQKMNLMM", "MOLLMM", "  FREE their IMPRISONED LEADER  "),
                new Audience(GroupType.Peasants, 0, 6/*S*/, "MPLMMMMM", "MMMLMM", "     START a PUBLIC LOTTERY     "),
                new Audience(GroupType.Landowners, 0, 0, "KMPMMMMM", "LMMMMM", "STOP MILITARY USE of THEIR LAND "),
                new Audience(GroupType.Landowners, 0, 0, "MIQMLMLM", "MKONMM", "  LOWER the BASIC MINIMUM WAGE  "),
                new Audience(GroupType.Landowners, 10/*W*/, -5/*H*/, "MMPMNMOI", "MMNMMM", "NATIONALISE AMERICAN BUSINESSES "),
                new Audience(GroupType.Landowners, 0, 5/*R*/, "MMPMJMLM", "MNOMLM", "LEVY DUTY on ALL LEFTOTO IMPORTS"),
                new Audience(GroupType.Landowners, 0, 4/*Q*/, "NNPMMIMM", "NMNNMK", " CUT SPENDING on the S. POLICE  "),
                new Audience(GroupType.Landowners, 0, -5/*H*/, "MMQMMMMM", "MMOMMM", "  DECREASE HEAVY LAND TAXATION  "),
                new Audience(GroupType.Landowners, 0, 0, "KLPMMMMM", "LLNNMM", "RELEASE TROOPS to WORK the LAND "),
                new Audience(GroupType.Landowners, -12/*A*/, -10/*C*/, "NNPMJMON", "MMPMKM", "BUILD a LARGE IRRIGATION SYSTEM "),
            };
        }

        public IEnumerable<Audience> GetUnusedAudiences()
        {
            IEnumerable<Audience> unusedAudiences = audiences.Where(x => !x.HasBeenUsed);

            if (!unusedAudiences.Any())
            {
                ResetAllToUnused();
                unusedAudiences = ((Audience[])audiences.Clone()).AsEnumerable();
            }

            return unusedAudiences;
        }

        private void ResetAllToUnused()
        {
            foreach (var audience in audiences)
            {
                audience.HasBeenUsed = false;
            }
        }
    }
}
