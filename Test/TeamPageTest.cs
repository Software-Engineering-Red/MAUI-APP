using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Test
{
    public class TeamPageTest
    {
        [Fact]
        public void SaveButton_Clicked_AddsNewTeam()
        {
            // Arrange
            var teamPage = new TeamPage();
            var teamServiceMock = new Mock<ITeamService>();
            teamPage._TeamService = teamServiceMock.Object;

            // Act
            teamPage.txe_team.Text = "New Team";
            teamPage.SaveButton_Clicked(null, null);

            // Assert
            teamServiceMock.Verify(service => service.AddTeam(It.IsAny<Team>()), Times.Once);
        }

        [Fact]
        public void SaveButton_Clicked_UpdatesExistingTeam()
        {
            // Arrange
            var teamPage = new TeamPage();
            var teamServiceMock = new Mock<ITeamService>();
            teamPage._TeamService = teamServiceMock.Object;

            var existingTeam = new Team { ID = 1, Name = "Old Name" };
            teamPage._selectedTeam = existingTeam;

            // Act
            teamPage.txe_team.Text = "Updated Team";
            teamPage.SaveButton_Clicked(null, null);

            // Assert
            teamServiceMock.Verify(service => service.UpdateTeam(existingTeam), Times.Once);
            Assert.Equal("Updated Team", existingTeam.Name);
        }

        [Fact]
        public void DeleteButton_Clicked_DeletesSelectedTeam()
        {
            // Arrange
            var teamPage = new TeamPage();
            var teamServiceMock = new Mock<ITeamService>();
            teamPage._TeamService = teamServiceMock.Object;

            var selectedTeam = new Team { ID = 1, Name = "Selected Team" };
            teamPage._selectedTeam = selectedTeam;
            teamPage._Teams = new ObservableCollection<Team> { selectedTeam };

            // Act
            teamPage.DeleteButton_Clicked(null, null);

            // Assert
            teamServiceMock.Verify(service => service.DeleteTeam(selectedTeam), Times.Once);
            Assert.DoesNotContain(selectedTeam, teamPage._Teams);
        }

        [Fact]
        public void ltv_Teams_ItemSelected_SetsSelectedTeamNameInTextBox()
        {
            // Arrange
            var teamPage = new TeamPage();
            var selectedTeam = new Team { ID = 1, Name = "Selected Team" };

            // Act
            teamPage.ltv_Teams_ItemSelected(null, new SelectedItemChangedEventArgs(selectedTeam, -1));

            // Assert
            Assert.Equal("Selected Team", teamPage.txe_team.Text);
        }
    }
}
