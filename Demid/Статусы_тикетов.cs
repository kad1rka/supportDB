//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Demid
{
    using System;
    using System.Collections.Generic;
    
    public partial class Статусы_тикетов
    {
        public int Идентификатор { get; set; }
        public Nullable<int> Идентификатор_тикета { get; set; }
        public string Название { get; set; }
        public string Описание { get; set; }
    
        public virtual Тикеты Тикеты { get; set; }
    }
}
