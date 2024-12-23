using System;
using System.Collections.Generic;
using Xunit;
using ProjectManagementApp;

namespace ProjectManagementApp.Tests
{
    public class ProjectTests
    {
        // AddTeamMember test
        [Fact]
        public void AddTeamMember_ShouldAddTeamMemberToTheList()
        {
            // Arrange
            var project = new Project(1, "Test Project", "Manager1", "Test description"); // initialize project details
            var newMember = "Janis";

            // Act
            project.AddTeamMember(newMember); // calling the method

            // Assert
            Assert.Contains(newMember, project.TeamMembers); // compare if project TeamMembers contains the new member
        }

        // UpdateStatus test
        [Fact]
        public void UpdateStatus_ShouldUpdateProjectStatus()
        {
            // Arrange
            var project = new Project(1, "Test Project", "Manager1", "Test description");
            var newStatus = ProjectStatus.InProgress; // setting new status

            // Act
            project.UpdateStatus(newStatus); // call the method

            // Assert
            Assert.Equal(newStatus, project.Status); // compare result
        }
    }
}
