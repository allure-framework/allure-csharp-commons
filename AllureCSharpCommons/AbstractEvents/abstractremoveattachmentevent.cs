namespace AllureCSharpCommons.AbstractEvents
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="abstract-remove-attachment-event", Namespace="urn:events.allure.qatools.yandex.ru")]
    [System.Xml.Serialization.XmlRootAttribute("remove-attachment-event", Namespace="urn:events.allure.qatools.yandex.ru", IsNullable=false)]
    public abstract partial class abstractremoveattachmentevent {
        
        private pattern patternField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public pattern pattern {
            get {
                return this.patternField;
            }
            set {
                this.patternField = value;
            }
        }
    }
}