using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests {
    public class ActionTest {

        private readonly ITestOutputHelper output;

        public ActionTest(ITestOutputHelper output) {
            this.output = output;
            //output.WriteLine(...);
        }

        [Fact]
        public void CreateAction_Test() {
            Action action = new Think();
            Assert.NotEqual(null, action);
        }

        [Fact]
        public void OnCreate_HasEmptyList_Test() {
            Action action = new Think();
            Assert.Equal(0, action.ActionListCount());
        }

        [Fact]
        public void OnCreate_HasStatusInactive_Test() {
            Action action = new Think();
            Assert.Equal(ActionEnum.STATUS_INACTIVE, action.Status);
        }

        [Fact]
        public void AfterActivate_HasStatusActive_Test() {
            Action action = new Think();
            action.Activate();
            Assert.Equal(ActionEnum.STATUS_ACTIVE, action.Status);
        }

        [Fact]
        public void AfterAddAction_HasOneInList_Test() {
            Action action = new Think();
            action.AddAction(new IdleAction());
            Assert.Equal(1, action.ActionListCount());
        }

        [Fact]
        public void AfterAddAction_HasAction_Test() {
            Action action = new Think();
            Action idleAction = new IdleAction();
            action.AddAction(idleAction);
            Assert.True(action.CurrentAction().Equals(idleAction));
        }

        [Fact]
        public void AfterAddTwoActions_HasTwoInList_Test() {
            Action action = new Think();
            Action idleAction = new IdleAction();
            Action wanderAction = new WanderAction();
            action.AddAction(idleAction);
            action.AddAction(wanderAction);
            Assert.Equal(2, action.ActionListCount());
        }

        [Fact]
        public void AfterAddTwoActions_CurrentActionIs_LastAddedAction_Test() {
            Action action = new Think();
            Action idleAction = new IdleAction();
            Action wanderAction = new WanderAction();
            action.AddAction(idleAction);
            action.AddAction(wanderAction);
            Assert.True(action.CurrentAction().Equals(wanderAction));
        }

        [Fact]
        public void AfterAddTwoActions_NextActionIs_FirstAddedAction_Test() {
            Action action = new Think();
            Action idleAction = new IdleAction();
            Action wanderAction = new WanderAction();
            action.AddAction(idleAction);
            action.AddAction(wanderAction);
            Assert.True(action.NextAction().Equals(idleAction));
        }

        [Fact]
        public void RemoveAction_OnEmptyList_GivesException_Test() {
            Action action = new Think();
            Assert.Throws<InvalidOperationException>(() => action.RemoveAction());
        }

        [Fact]
        public void RemoveAction_OnListWithOneAction_Test() {
            Action action = new Think();
            Action idleAction = new IdleAction();
            action.AddAction(idleAction);
            action.RemoveAction();
            Assert.Equal(0, action.ActionListCount());
        }

        [Fact]
        public void RemoveAction_OnListWithTwoActions_CurrentActionIsFirstAddedAction_Test() {
            Action action = new Think();
            Action idleAction = new IdleAction();
            Action wanderAction = new WanderAction();
            action.AddAction(idleAction);
            action.AddAction(wanderAction);
            action.RemoveAction();
            Assert.True(action.CurrentAction().Equals(idleAction));
        }

        [Fact]
        public void AddTwoActionsByList_Test() {
            Action action = new Think();
            List<Action> list = new List<Action>();
            Action idleAction = new IdleAction();
            Action wanderAction = new WanderAction();
            list.Add(idleAction);
            list.Add(wanderAction);
            action.AddActions(list);
            Assert.Equal(2, action.ActionListCount());
        }

        [Fact]
        public void AddTwoActionsByList_LastAddedIsCurrentAction_Test() {
            Action action = new Think();
            List<Action> list = new List<Action>();
            Action idleAction = new IdleAction();
            Action wanderAction = new WanderAction();
            list.Add(idleAction);
            list.Add(wanderAction);
            action.AddActions(list);
            Assert.True(action.CurrentAction().Equals(wanderAction));
        }
    }
}
