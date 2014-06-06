//------------------------------------------------------------------------------
// <auto-generated>
//     ���� ��� ������ ����������.
//     ����������� ������:4.0.30319.17929
//
//     ��������� � ���� ����� ����� �������� � ������������ ������ � ����� �������� � ������
//     ��������� ��������� ����.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.0.30319.17929.
// 


namespace AllureCSharpCommons.AllureModel
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "test-suite-result", Namespace = "urn:model.allure.qatools.yandex.ru")]
    [System.Xml.Serialization.XmlRootAttribute("test-suite", Namespace = "urn:model.allure.qatools.yandex.ru", IsNullable = false)]
    public partial class testsuiteresult
    {

        private string nameField;

        private string titleField;

        private description descriptionField;

        private testcaseresult[] testcasesField;

        private label[] labelsField;

        private long startField;

        private long stopField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public description description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute("test-cases", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("test-case", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public testcaseresult[] testcases
        {
            get
            {
                return this.testcasesField;
            }
            set
            {
                this.testcasesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public label[] labels
        {
            get
            {
                return this.labelsField;
            }
            set
            {
                this.labelsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public long start
        {
            get
            {
                return this.startField;
            }
            set
            {
                this.startField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public long stop
        {
            get
            {
                return this.stopField;
            }
            set
            {
                this.stopField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:model.allure.qatools.yandex.ru")]
    public partial class description
    {

        private descriptiontype typeField;

        private string valueField;

        public description()
        {
            this.typeField = descriptiontype.text;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(descriptiontype.text)]
        public descriptiontype type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "description-type", Namespace = "urn:model.allure.qatools.yandex.ru")]
    public enum descriptiontype
    {

        /// <remarks/>
        markdown,

        /// <remarks/>
        text,

        /// <remarks/>
        html,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:model.allure.qatools.yandex.ru")]
    public partial class parameter
    {

        private string nameField;

        private string valueField;

        private parameterkind kindField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public parameterkind kind
        {
            get
            {
                return this.kindField;
            }
            set
            {
                this.kindField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "parameter-kind", Namespace = "urn:model.allure.qatools.yandex.ru")]
    public enum parameterkind
    {

        /// <remarks/>
        argument,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("system-property")]
        systemproperty,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("environment-variable")]
        environmentvariable,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:model.allure.qatools.yandex.ru")]
    public partial class label
    {

        private string nameField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:model.allure.qatools.yandex.ru")]
    public partial class attachment
    {

        private string titleField;

        private string sourceField;

        private string typeField;

        private int sizeField;

        private bool sizeFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int size
        {
            get
            {
                return this.sizeField;
            }
            set
            {
                this.sizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool sizeSpecified
        {
            get
            {
                return this.sizeFieldSpecified;
            }
            set
            {
                this.sizeFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:model.allure.qatools.yandex.ru")]
    public partial class step
    {

        private string nameField;

        private string titleField;

        private attachment[] attachmentsField;

        private step[] stepsField;

        private long startField;

        private long stopField;

        private status statusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public attachment[] attachments
        {
            get
            {
                return this.attachmentsField;
            }
            set
            {
                this.attachmentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public step[] steps
        {
            get
            {
                return this.stepsField;
            }
            set
            {
                this.stepsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public long start
        {
            get
            {
                return this.startField;
            }
            set
            {
                this.startField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public long stop
        {
            get
            {
                return this.stopField;
            }
            set
            {
                this.stopField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public status status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:model.allure.qatools.yandex.ru")]
    public enum status
    {

        /// <remarks/>
        failed,

        /// <remarks/>
        broken,

        /// <remarks/>
        passed,

        /// <remarks/>
        canceled,

        /// <remarks/>
        pending,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:model.allure.qatools.yandex.ru")]
    public partial class failure
    {

        private string messageField;

        private string stacktraceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("stack-trace", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string stacktrace
        {
            get
            {
                return this.stacktraceField;
            }
            set
            {
                this.stacktraceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "test-case-result", Namespace = "urn:model.allure.qatools.yandex.ru")]
    public partial class testcaseresult
    {

        private string nameField;

        private string titleField;

        private description descriptionField;

        private failure failureField;

        private step[] stepsField;

        private attachment[] attachmentsField;

        private label[] labelsField;

        private parameter[] parametersField;

        private long startField;

        private long stopField;

        private status statusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public description description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public failure failure
        {
            get
            {
                return this.failureField;
            }
            set
            {
                this.failureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public step[] steps
        {
            get
            {
                return this.stepsField;
            }
            set
            {
                this.stepsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public attachment[] attachments
        {
            get
            {
                return this.attachmentsField;
            }
            set
            {
                this.attachmentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public label[] labels
        {
            get
            {
                return this.labelsField;
            }
            set
            {
                this.labelsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public parameter[] parameters
        {
            get
            {
                return this.parametersField;
            }
            set
            {
                this.parametersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public long start
        {
            get
            {
                return this.startField;
            }
            set
            {
                this.startField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public long stop
        {
            get
            {
                return this.stopField;
            }
            set
            {
                this.stopField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public status status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
    }
    
}