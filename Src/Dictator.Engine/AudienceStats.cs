using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class AudienceStats
    {
        private Audience[] audiences;

        public AudienceStats()
        {
            Initialise();
        }

        public void Initialise()
        {
            audiences = new Audience[] {
                new Audience(GroupType.Army, 0, 0/*H*/, "QJLMMMMM", "PKLMMM", "     INTRODUCE CONSCRIPTION     "),
                new Audience(GroupType.Army, 0, 0, "PMJMMMMM", "NMLMMM", " REQUISITION LAND for TRAINING  "),
                new Audience(GroupType.Army, 0/*C*/, 0, "PLNMLMLM", "NMNIMM", "   ATTACK ALL GUERILLA BASES    "),
                new Audience(GroupType.Army, 0/*E*/, 0, "PLMMIMLM", "NMNKMM", "ATTACK GUERILLA BASES in LEFTOTO"),
                new Audience(GroupType.Army, 0, 0, "QONMMIMM", "NMNMMJ", "  SACK the SECRET POLICE CHIEF  "),
                new Audience(GroupType.Army, 0, 0, "PMMMLMIO", "MMMMMM", "EXPEL RUSSIAN MILITARY ADVISORS "),
                new Audience(GroupType.Army, 0, 0/*D*/, "QMLMMMMM", "OLLLMM", " INCREASE the PAY of the TROOPS "),
                new Audience(GroupType.Army, 0/*A*/, 0, "QLLMLLMM", "PLLKLM", "  BUY MORE ARMS and AMMUNITION  "),

                // TODO: add peasants and land owners petition data
            };
        }

    }
}
