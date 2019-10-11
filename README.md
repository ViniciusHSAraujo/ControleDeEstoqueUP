# Controle de Estoque UP

O Controle de Estoque UP é um sistema de controle de estoque voltado para pequenas empresas para que possam fazer o controle de seu estoque a partir do lançamento de suas compras e vendas, desenvolvido como parte da avaliação da disciplina de "Desenvolvimento de Sistemas Microsoft" na Universidade Positivo.

O sistema conta também com o controle de clientes, fornecedores, funcionários, e o cadastro de produtos com suas categorias e unidades de medidas para facilitar a busca e organização de seu estoque.

Ao cadastrar uma compra, o sistema grava no estoque cada produto com seu código de identificação, como forma de "identificação" do próprio no sistema.

Com nosso sistema, você tem acesso fácil aos dados de seus funcionários, clientes e fornecedores, além de poder atualizar os dados dos mesmos de maneira bem simplificada.

O controle de acesso do sistema se dará por meio de uma senha que deverá ser definida no cadastro do funcionário no sistema.

Além de ter de forma automatizada a atualização dos saldos dos itens no estoque, o usuário pode definir qual é custo médio de seus produtos, e com isso definir com melhor precisão o preço de venda de seus itens, evitando prejuízos e aumentando a lucratividade.

## Demonstração
Login

![Login](https://image.prntscr.com/image/_1F-COKcRyuZ6Qvr1i-uHQ.png)

Cadastro

![Cadastro](https://image.prntscr.com/image/s5wY1V44TBKb5PKd3I6u5Q.png)

Pesquisa

![Pesquisa](https://image.prntscr.com/image/KybB_LDnRYKOTYmOaDyNgw.png)

Compra

![Compra](https://image.prntscr.com/image/DFnHeTiITD6-gkNZc29hcA.png)

## Instalação

Faça o download do código fonte em sua máquina, abra o projeto no VisualStudio (utilizado o 2019 para o desenvolvimento) e crie o banco de dados com o Entity Framework:

``` net
update-database
```

## Primeiro Acesso

```
Para realizar o primeiro acesso, utilize as credenciais:
Usuário: 0
Senha: CEUP123
```

## Contribuindo
Solicitações pull são bem-vindas. Para grandes mudanças, abra um "issue" primeiro para discutir o que você gostaria de mudar.

Desenvolva ou atualize os testes conforme apropriado.

## Licença
[MIT] (https://choosealicense.com/licenses/mit/)
