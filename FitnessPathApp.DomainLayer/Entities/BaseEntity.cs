using FitnessPathApp.DomainLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessPathApp.DomainLayer.Entities
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}
