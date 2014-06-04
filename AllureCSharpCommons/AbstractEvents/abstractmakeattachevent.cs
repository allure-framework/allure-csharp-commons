namespace AllureCSharpCommons
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="abstract-make-attach-event", Namespace="urn:events.allure.qatools.yandex.ru")]
    [System.Xml.Serialization.XmlRootAttribute("make-attach-event", Namespace="urn:events.allure.qatools.yandex.ru", IsNullable=false)]
    public abstract partial class abstractmakeattachevent {
        
        private string titleField;
        
        private attachmenttype attachmentTypeField;
        
        private attachment attachField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string title {
            get {
                return this.titleField;
            }
            set {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public attachmenttype attachmentType {
            get {
                return this.attachmentTypeField;
            }
            set {
                this.attachmentTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public attachment attach {
            get {
                return this.attachField;
            }
            set {
                this.attachField = value;
            }
        }
    }
}