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
    
    public partial class Тикеты
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Тикеты()
        {
            this.Журналы_логов = new HashSet<Журналы_логов>();
            this.История_действий = new HashSet<История_действий>();
            this.Решения = new HashSet<Решения>();
            this.Статусы_тикетов = new HashSet<Статусы_тикетов>();
        }
    
        public int Идентификатор { get; set; }
        public Nullable<int> Идентификатор_пользователя { get; set; }
        public string Тема { get; set; }
        public Nullable<int> Идентификатор_проблемы { get; set; }
        public string Статус { get; set; }
        public string Приоритет { get; set; }
        public Nullable<System.DateTime> Дата_создания { get; set; }
        public Nullable<System.DateTime> Дата_обновления { get; set; }
        public Nullable<int> Идентификатор_ответственного { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Журналы_логов> Журналы_логов { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<История_действий> История_действий { get; set; }
        public virtual Категории_проблем Категории_проблем { get; set; }
        public virtual Ответственные Ответственные { get; set; }
        public virtual Пользователи Пользователи { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Решения> Решения { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Статусы_тикетов> Статусы_тикетов { get; set; }
    }
}
