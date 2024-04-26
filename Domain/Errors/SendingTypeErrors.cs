using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    public static class SendingTypeErrors
    {
        public static Error SendingTypeNotFoundById(int id) =>
            Error.NotFound("SendingType.NotFoundById", $"Sending type with ID {id} was not found.");

        public static Error SendingTypeNotFoundByName(string name) =>
            Error.NotFound("SendingType.NotFoundByName", $"Sending type with name {name} was not found.");

        public static Error SendingTypeAlreadyExistByName(string name) =>
            Error.Conflict("SendingType.AlreadyExistByName", $"Sending type with name {name} already exists.");
    }
}
