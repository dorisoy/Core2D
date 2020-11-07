﻿using Core2D;
using Xunit;

namespace Core2D.Data.UnitTests
{
    public class ColumnTests
    {
        private readonly IFactory _factory = new Factory();

        [Fact]
        [Trait("Core2D.Data", "Database")]
        public void Inherits_From_ViewModelBase()
        {
            var db = _factory.CreateDatabase("db");
            var target = _factory.CreateColumn(db, "Column");
            Assert.True(target is ViewModelBase);
        }
    }
}
