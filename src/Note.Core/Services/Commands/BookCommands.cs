﻿using Note.Core.Enums;
using Note.Core.Services.Commands.Base;
using System;

namespace Note.Core.Services.Commands
{
    public class CreateBookCommand : ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Access ReadAccess { get; set; }
        public Access WriteAccess { get; set; }

        public CreateBookCommand(string name, string description, Access readAccess, Access writeAccess)
        {
            Name = name;
            Description = description;
            ReadAccess = readAccess;
            WriteAccess = writeAccess;
        }

        public bool IsValid 
        { 
            get
            {
                if(string.IsNullOrEmpty(Name) || Name.Length > 250)
                {
                    return false;
                }

                return true;
            }
        }

    }

    public class UpdateBookCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Access ReadAccess { get; set; }
        public Access WriteAccess { get; set; }

        public UpdateBookCommand(Guid id, string name, string description, Access readAccess, Access writeAccess)
        {
            Id = id;
            Name = name;
            Description = description;
            ReadAccess = readAccess;
            WriteAccess = writeAccess;
        }

        public bool IsValid
        {
            get
            {
                if(Id == Guid.Empty)
                {
                    return false;
                }

                if (string.IsNullOrEmpty(Name) || Name.Length > 250)
                {
                    return false;
                }

                return true;
            }
        }
    }
}