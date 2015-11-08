namespace BecomeSolid.Refactoring.Day1.Builders
{
    public class MessageBuilder
    {
        private string _name;
        private string _description;
        private double _temperature;

        public MessageBuilder InCity(string name)
        {
            this._name = name;
            return this;
        }

        public MessageBuilder WithDescription(string description)
        {
            this._description = description;
            return this;
        }

        public MessageBuilder WithTemperature(double temperature)
        {
            this._temperature = temperature;
            return this;
        }

        public string Build()
        {
            var message = "In " + _name + " " + _description + " and the temperature is " +
                          _temperature.ToString("+#;-#") + "°C";
            return message;
        }
    }
}