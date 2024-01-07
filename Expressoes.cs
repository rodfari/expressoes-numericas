namespace expressoes_numericas;
public class Expressoes
{
    //public int Total { get { return total; } private set { } }
    private readonly string exp;
    public Expressoes(string exp)
    {
        this.exp = exp;
    }
    private char searchFor = '(';
    public string getInner()
    {
        int index = exp.IndexOf(searchFor) + 1;
        if (index == 0)
        {
            if (searchFor == ')')
                return exp;

            searchFor = ')';
            return getInner();
        }
        int length = exp.Length - index;
        string sub = string.Empty;

        if (searchFor == '(')
            sub = exp.Substring(index, length);
        else
            sub = exp.Substring(0, index - 1);

        return getInner();
    }

    //private int total = 0;
    public string CalcularValorInterno(string value)
    {
        char[] operadores = new[] { '*', '/', '+', '-' };

        //REALIZA BUSCAR POR SIMBOLOS DE * OU / PARA OBTER A POSICAO DO SIMBOLO.
        int symbolIndex = value.IndexOfAny(new char[] { '*', '/' });
        
        //CASO NÃO HAJA NENHUM SIMBOLO DE * OU /, ENTÃO PROCURAR POR POR SINAIS DE + OU -.
        if (symbolIndex == -1)
            symbolIndex = value.IndexOfAny(new char[] { '+', '-' });

        //CASO NÃO HAJA NENHUM SINAL, ENTÃO FIM. ???? PROVALMENTE ERRADO
        if (symbolIndex == -1)
            return value;

        //OBTER O SINAL DA PRÓXIMA OPERACAO
        string sinalOperacao = value.Substring(symbolIndex, 1);

        //OBTER OS CARACTERES DO LADO ESQUERDO DO SIMBOLO DA OPERACAO.
        string primeiroPedaco = value.Substring(0, symbolIndex);

        //OBTER OS CARACTERES DO LADO DIREITO DO SIMBOLO DA OPERACAO.
        string segundoPedaco = value.Substring(symbolIndex + 1);

        //ARMAZENAR O PRIMEIRO VALOR DA OPERACAO
        string primeiroValorOperacao = string.Empty;
        string segundoValorOperacao = string.Empty;

        //TODO - VERIFICAR SE EXISTE ALGUM SIMBOLO DO LADO ESQUERDO DO SIMBOLO.
        if (primeiroPedaco.Any(x => operadores.Contains(x) ))
        {
            //PARA ISSO É NECESSÁRIO REVERTER O PRIMEIRO PEDACO.
            string PrimeiroPedacoRevertido = new(primeiroPedaco.Reverse().ToArray());

            //OBTER A POSICAO DO SIMBOLO DA OPERACAO
            int indexDoPrimeiroPedacoRevertido = 
                PrimeiroPedacoRevertido.IndexOfAny(operadores);

            //OBTER O PRIMEIRO VALOR DA OPERACAO    
            primeiroValorOperacao = 
                    PrimeiroPedacoRevertido[..indexDoPrimeiroPedacoRevertido];

            //REVERTO PARA O NORMAL        
            primeiroValorOperacao = new(primeiroValorOperacao.Reverse().ToArray());        

            //OBTER O PRIMEIRO REMOVENDO O VALOR DA OPERACAO
            PrimeiroPedacoRevertido = 
                    PrimeiroPedacoRevertido[indexDoPrimeiroPedacoRevertido..];

            primeiroPedaco = new(PrimeiroPedacoRevertido.Reverse().ToArray());
        }
        else{
            primeiroValorOperacao = primeiroPedaco;
        }

        //SEGUNDO PEDACO
        //TODO - VERIFICAR SE EXISTE ALGUM SIMBOLO DO LADO DIREITO DO SIMBOLO.
        if (segundoPedaco.Any(x => operadores.Contains(x) ))
        {
            //PARA ISSO É NECESSÁRIO REVERTER O SEGUNDO PEDACO.
            //string SegundoPedacoRevertido = new(segundoPedaco.Reverse().ToArray());

            //OBTER A POSICAO DO SIMBOLO DA OPERACAO
            int indexDoSegundoPedaco = 
                segundoPedaco.IndexOfAny(operadores);

            //OBTER O PRIMEIRO VALOR DA OPERACAO    
            segundoValorOperacao = 
                    segundoPedaco[..indexDoSegundoPedaco];
             

            //OBTER O SEGUNDO PEDACO REMOVENDO O VALOR DA OPERACAO
            segundoPedaco = 
                    segundoPedaco[indexDoSegundoPedaco..];

            
        }
        else{
            segundoValorOperacao = segundoPedaco;
        }


        int.TryParse(primeiroValorOperacao, out int val1);
        int.TryParse(segundoValorOperacao, out int val2);
        int result = ObterValorOperacao(val1, val2, sinalOperacao);

        string res = string.Empty;
        if( !segundoPedaco.ToArray().Any(x => operadores.Contains(x))
            && !primeiroPedaco.Any(x => operadores.Contains(x))
        ){
            result = ObterValorOperacao(Convert.ToInt32(primeiroPedaco), int.Parse(segundoPedaco), sinalOperacao);
            return result.ToString();
        }
        if (!segundoPedaco.ToArray().Any(x => operadores.Contains(x)))
        {
            res = string.Concat(primeiroPedaco, result.ToString());
        }
        else if (!primeiroPedaco.Any(x => operadores.Contains(x)))
        {
            res = string.Concat(result.ToString(), segundoPedaco);
        }
        else{
            res = string.Concat(primeiroPedaco, result.ToString(), segundoPedaco);
        }
        return CalcularValorInterno(res);
    }

    private int ObterValorOperacao(int valor1, int valor2, string operador){
        int total = 0;
         if (operador.Equals("*"))
            total = valor1  * valor2;
        if (operador.Equals("/"))
            total = valor1  / valor2;
        if (operador.Equals("+"))
            total =  valor1 + valor2;
        if (operador.Equals("-"))
            total =  valor1 - valor2;

        return total;    
    }

    
}