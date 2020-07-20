namespace DatabaseSample.Helpers
{
    using System;
    using System.Data;

    using Smart.Data.Mapper.Handlers;

    public sealed class DateTimeTypeHandler : TypeHandler<DateTime>
    {
        public override void SetValue(IDbDataParameter parameter, DateTime value)
        {
            parameter.DbType = DbType.Int64;
            parameter.Value = value.Ticks;
        }

        public override DateTime Parse(object value)
        {
            return new DateTime((long)value);
        }
    }
}
