using System;
using System.Data.Entity.Migrations;
using BarnManagement.Data;

namespace BarnManagement.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BarnContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BarnContext context)
        {
            
        }
    }
}
