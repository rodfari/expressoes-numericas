using expressoes_numericas;

Expressoes expressoes = new Expressoes("23+(30*9)+(30-40(90*40+(30-2)))");

string valor = expressoes.CalcularValorInterno("10+1500*3/2-60");
Console.WriteLine(valor);

Console.WriteLine("Pressione Enter para finalizar!");
Console.Read();


