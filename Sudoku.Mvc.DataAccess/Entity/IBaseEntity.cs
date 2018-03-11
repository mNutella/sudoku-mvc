using System;

namespace Sudoku.Mvc.DataAccess.Entity
{
    public interface IBaseEntity
    {
        object Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        DateTime? DeletedDate { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
        string DeletedBy { get; set; }
        byte[] Version { get; set; }
    }

    public interface IBaseEntity<T> : IBaseEntity
    {
        new T Id { get; set; }
    }
}
