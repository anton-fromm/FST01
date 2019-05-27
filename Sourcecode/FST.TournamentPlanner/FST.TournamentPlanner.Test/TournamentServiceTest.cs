using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FST.TournamentPlanner.API.Repositories;
using FST.TournamentPlanner.API.Services;
using FST.TournamentPlanner.DB.Contexts;
using Xunit;

namespace FST.TournamentPlanner.Tests
{
    public class TournamentServiceTest //: IocTestBase
    {
        private readonly ITournamentService _tournamentService;
        private readonly IRepositoryWrapper _repositoryWrapper;


        public TournamentServiceTest()
        {
            this._tournamentService = new TournamentService(
                new RepositoryWrapper(
                    new PlannerContext(
                        "Data Source=stadtler-fst.database.windows.net;Initial Catalog=FST;Persist Security Info=True;User ID=User1;Password=123qwe!!"
                    )
                )
            );
        }


        [Fact]
        public void GenerateMatchTree_ShouldHaveMatchesOnTournament()
        {
            API.Models.Tournament tournament = this._tournamentService.GenerateMatchPlan(1);
            tournament.Matches.Should().HaveCountGreaterThan(0);
        }
    }
}