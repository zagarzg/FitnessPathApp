using FitnessPathApp.DomainLayer.Interfaces;
using System;

namespace FitnessPathApp.DomainLayer.Entities
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}
