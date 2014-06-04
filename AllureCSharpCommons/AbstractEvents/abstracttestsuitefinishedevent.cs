namespace AllureCSharpCommons.AbstractEvents
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="abstract-test-suite-finished-event", Namespace="urn:events.allure.qatools.yandex.ru")]
    [System.Xml.Serialization.XmlRootAttribute("test-suite-finished-event", Namespace="urn:events.allure.qatools.yandex.ru", IsNullable=false)]
    public abstract partial class abstracttestsuitefinishedevent {
        
        private string uidField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string uid {
            get {
                return this.uidField;
            }
            set {
                this.uidField = value;
            }
        }
    }
}