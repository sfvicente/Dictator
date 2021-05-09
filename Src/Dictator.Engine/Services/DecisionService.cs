﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dictator.Core.Services
{
    public class DecisionService : IDecisionService
    {
        private Decision[] decisions;

        public DecisionService()
        {
            Initialise();
        }

        public void Initialise()
        {
            decisions = new Decision[] {
                new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 0, 0, "QLLMMLMM", "NMMLML", "MAKE ARMY CHIEF \"VICE-PRESIDENT\""),
                new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, -1/*L*/, -4/*I*/, "LQNMOMNM", "MMMLMM", "SET UP FREE CLINICS for WORKERS "),
                new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 0, 0, "LKQMMLLM", "LLOMML", "GIVE LANDOWNERS REGIONAL POWERS "),
                new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 5/*R*/, 0, "KMMMQMKN", "LMMLPM", "SELL AMERICAN ARMS to LEFTOTO   "),
                new Decision(DecisionType.PleaseAGroup, DecisionSubType.None,12/*Y*/, 0, "MMLMLMKP", "MMMMMM", "SELL MINING RIGHTS to U.S. FIRMS"),
                new Decision(DecisionType.PleaseAGroup, DecisionSubType.None,0, 10/*W*/, "KMMMMMPJ", "MMMMMM", "RENT the RUSSIANS a NAVAL BASE  "),

                new Decision(DecisionType.PleaseAllGroups, DecisionSubType.None,0, -8/*E*/, "NPPMMMMM", "LMMLMM", "DECREASE GENERAL TAXATION LEVEL "),
                new Decision(DecisionType.PleaseAllGroups, DecisionSubType.None, -8/*E*/, 0, "PPPMMMMM", "MMMLMM", "STAGE a BIG POPULARITY CAMPAIGN "),
                new Decision(DecisionType.PleaseAllGroups, DecisionSubType.None,0, 8/*U*/, "PPPMMDMM", "ONNNMD", "CUT S.POLICE POWERS COMPLETELY  "),

                new Decision(DecisionType.ImproveYourChanges, DecisionSubType.None, 0, -6/*G*/, "JJJMMUMM", "LLLLMU", "INCREASE S.POLICE POWERS a LOT  "),
                new Decision(DecisionType.ImproveYourChanges, DecisionSubType.IncreaseBodyGuard, -4/*I*/, 0, "KLLMMLMM", "KMMMML", "INCREASE YOUR BODYGUARD        *"),
                new Decision(DecisionType.ImproveYourChanges, DecisionSubType.PurchaseHelicopter, -12/*A*/, 0, "IIJMMKMM", "MMMMMM", "BUY an ESCAPE HELICOPTER        "),
                new Decision(DecisionType.ImproveYourChanges, DecisionSubType.TransferToSwissAccount, 0, 0, "MMMMMMMM", "MMMMMM", "SEE TO YOUR SWISS BANK ACCOUNT *"),

                new Decision(DecisionType.RaiseSomeCash, DecisionSubType.AskForRussianLoan, 0, 0, "MMMMMMMM", "MMMMMM", "ASK the RUSSIANS for a \"LOAN\"  *"),
                new Decision(DecisionType.RaiseSomeCash, DecisionSubType.AskForAmericanLoan, 0, 0, "MMMMMMMM", "MMMMMM", "ASK AMERICANS for FOREIGN \"AID\"*"),
                new Decision(DecisionType.RaiseSomeCash, DecisionSubType.None, 12/*Z*/, 0, "NNPMGMKM", "MMMMMM", "NATIONALISE LEFTOTAN BUSINESSES "),

                new Decision(DecisionType.StrengthenAGroup, DecisionSubType.None, -5/*H*/, 0, "PMMMJMLM", "RMMKKL", "BUY HEAVY ARTILLERY for THE ARMY"),
                new Decision(DecisionType.StrengthenAGroup, DecisionSubType.None,0, 0, "MPLMMLMM", "MRLPML", "ALLOW PEASANTS FREE MOVEMENT    "),
                new Decision(DecisionType.StrengthenAGroup, DecisionSubType.None,0, 0, "LLPMMLMM", "LLRLML", "ALLOW LANDOWNERS PRIVATE MILITIA")
            };
        }

        public Decision[] GetDecisionsByType(DecisionType decisionType)
        {
            Decision[] decisionCopy = (Decision[])decisions.Clone();

            return decisionCopy.Where(x => x.Type == decisionType).ToArray();
        }

        public void MarkDecisionAsUsed(string text)
        {
            Decision item = decisions.Where(x => x.Text == text).Single();

            item.HasBeenUsed = true;
        }
    }
}