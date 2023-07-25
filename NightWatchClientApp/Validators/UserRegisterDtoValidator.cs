using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
﻿using FluentValidation;

namespace NightWatchClientApp.Validators;


public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidator()
    {
        RuleFor(u => u.fullName)
            .NotEmpty().WithMessage("имя не должно быть пустым")
            .Length(3, 30).WithMessage("{PropertyName} {TotalLength} pls provide valid message")
            .Must(BeAValidName).WithMessage("{PropertyName} no digits");



        RuleFor(u => u.password)
            .NotEmpty().WithMessage("{PropertyName} shouldnt be empty")
            .Length(3, 20).WithMessage("{PropertyName} pls provide valid message")
            .Must(BeAValidName).WithMessage("{PropertyName} no digits");

        RuleFor(u => u.email)
            .NotEmpty().WithMessage("{PropertyName} shouldnt be empty")
            .Length(1, 10).WithMessage("{PropertyName} pls provide valid message")
            .Must(BeAValidName).WithMessage("{PropertyName} no digits");

    }


    protected bool BeAValidName(string name)
    {
        name = name.Replace(" ", "");
        name = name.Replace("-", "");
        return name.All(char.IsLetter);
    }

}