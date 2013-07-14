using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace BDD.Common
{
    [Binding]
    public class ForEveryScenario
    {
        private readonly Stack<Action> _atEndOfScenario = new Stack<Action>();

        [Before]
        public void SetUp()
        {
            ScenarioContext.Current.Set<ForEveryScenario>(this);
        }

        [After]
        public void TearDown()
        {
            CauseScenarioToFailInCaseOfErrors(_atEndOfScenario.Aggregate("", (curr, a) => curr + ExecuteWithCatchAll(a)));
        }

        private void CauseScenarioToFailInCaseOfErrors(string errors)
        {
            if (string.IsNullOrEmpty(errors)) return;
            throw new Exception(errors);
        }

        private string ExecuteWithCatchAll(Action a)
        {
            try
            {
                a.Invoke();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Exception: {0}\n{1}", ex, ex.StackTrace);
                return ex.ToString();
            }
            return "";
        }

        public static void AtEndExecute(Action a)
        {
            ScenarioContext.Current.Get<ForEveryScenario>()._atEndOfScenario.Push(a);
        }
    }
}