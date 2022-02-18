using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessPathApp.DomainLayer.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
