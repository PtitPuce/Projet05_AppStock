using System;

namespace AppStock.Models
{
    public interface ITimedEntity
    {
        DateTime CreatedAt {get; set;}
        DateTime UpdatedAt {get; set;}
    }
}