using Inspire.Annotator.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Inspire.Modeller
{
    [Button(ButtonType.Approve)]
    public class ModifierChecker<T>: Modifier<T>
        where T: IEquatable<T>
    {
        public DateTime? DateAuthorised { get; set; }
        [StringLength(60)]
        public string AuthorisedBy { get; set; }
        [StringLength(2)]
        public string AuthStatus { get; set; }
        public int AuthCount { get; set; }
    }
    public class ModifierCheckerDto<T> : ModifierDto<T>
        where T : IEquatable<T>
    {
    }
}
