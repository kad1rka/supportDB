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
    
    public partial class Помещения_и_оборудование
    {
        public int Идентификатор { get; set; }
        public string Номер_помещения { get; set; }
        public Nullable<int> Идентификатор_оборудования { get; set; }
        public string Состояние { get; set; }
        public Nullable<System.DateTime> Дата_установки { get; set; }
        public string Описание { get; set; }
    
        public virtual Оборудование Оборудование { get; set; }
    }
}
