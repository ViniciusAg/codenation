using System;

namespace Codenation.Challenge
{
    public class Country
    {
        public State[] Top10StatesByArea()
        {
            State[] States = new State[10];

            States[0] = new State("Amazonas", "AM");
            States[1] = new State("Pará", "PA");
            States[2] = new State("Mato Grosso", "MT");
            States[3] = new State("Minas Gerais", "MG");
            States[4] = new State("Bahia", "BA");
            States[5] = new State("Mato Grosso do Sul", "MS");
            States[6] = new State("Goiás", "GO");
            States[7] = new State("Maranhão", "MA");
            States[8] = new State("Rio Grande do Sul", "RS");
            States[9] = new State("Tocantis", "TO");

            return States;
        }
    }
}