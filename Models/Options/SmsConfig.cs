namespace Models.Options
{
    public class SmsConfig
    {
        /// <summary>
        ///     待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为1000个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
        /// </summary>
        public string PhoneNumbers { get; set; }

        /// <summary>
        ///     短信模板-可在短信控制台中找到
        /// </summary>
        public string TemplateCode { get; set; }

        /// <summary>
        ///     模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
        /// </summary>
        public string TemplateParam { get; set; }

        /// <summary>
        ///     outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者
        /// </summary>
        public string OutId { get; set; }
    }
}