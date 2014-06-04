namespace AllureCSharpCommons
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="abstract-test-case-status-change-event", Namespace="urn:events.allure.qatools.yandex.ru")]
    [System.Xml.Serialization.XmlRootAttribute("test-case-status-change-event", Namespace="urn:events.allure.qatools.yandex.ru", IsNullable=false)]
    public abstract partial class abstracttestcasestatuschangeevent {
        
        private throwable throwableField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public throwable throwable {
            get {
                return this.throwableField;
            }
            set {
                this.throwableField = value;
            }
        }
    }
}