using FluentValidation;
using Laborator2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator2.Validators
{
    public class CommentValidator : AbstractValidator<Comment>
    {

		public CommentValidator()
		{

			RuleFor(x => x.Text).NotEmpty()
				.MinimumLength(3)
				.MaximumLength(100);

			RuleFor(x => x.Author).NotEmpty()
				.MinimumLength(3)
				.MaximumLength(50);

		}
	}
}
