using System.Data.Linq.Mapping;

namespace CommonLibrary.Model
{
    [Table(Name = "dbo.Emails")]
    public partial class Email
    {

        private string _Value;

        private string _Name;

        public Email()
        {
        }

        [Column(Storage = "_Value", DbType = "NVarChar(MAX) NOT NULL", CanBeNull = false)]
        public string Value
        {
            get
            {
                return this._Value;
            }
            set
            {
                if ((this._Value != value))
                {
                    this._Value = value;
                }
            }
        }

        [Column(Storage = "_Name", DbType = "NVarChar(MAX) NOT NULL", CanBeNull = false)]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this._Name = value;
                }
            }
        }
    }
}
