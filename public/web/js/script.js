
document.getElementById('entradaForm').addEventListener('submit', function(event){
    event.preventDefault();
    
    const data = document.getElementById('data').value;
    // var split = data.value.split('-');
    // dataCompleta = split[2] + '-' + split[1] + '-' + split[0];
    const valor = document.getElementById('valor').value;
    const descricao = document.getElementById('descricao').value;
    const tipo = document.getElementById('tipo').value;
    const categoria = document.getElementById('categoria').value;
    const idUser = document.getElementById('idUser').value;

    
    const registro = {
        date: data,
        valor: valor,
        descricao: descricao,
        tipo: tipo,
        categoria: categoria,
        idUser: idUser
    };
    
    console.log(registro);

    fetch('http://localhost:5109/Finance/Post', {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(registro)
    })
    .then(response => response.json())
    .then(data => {
        alert('Entrada registrada com sucesso!');
        listarTransacoes();
    })
    .catch(error => {
        console.error('Erro:', error);
    });
});


// document.getElementById('saidaForm').addEventListener('submit', function(event) {
//     event.preventDefault();
//     const valor = document.getElementById('valorSaida').value;
//     const descricao = document.getElementById('descricaoSaida').value;
//     const categoria = document.getElementById('categoriaSaida').value;

//     const saidaData = {
//         valor: parseFloat(valor),
//         descricao: descricao,
//         tipo: "Saida",
//         categoria: categoria
//     };

//     fetch('http://localhost:5109/Financeiro/Saida', {
//         method: 'POST',
//         headers: {
//             'Content-Type': 'application/json'
//         },
//         body: JSON.stringify(saidaData)
//     })
//     .then(response => response.json())
//     .then(data => {
//         alert('Saída registrada com sucesso!');
//         listarTransacoes();
//     })
//     .catch(error => {
//         console.error('Erro:', error);
//     });
// });

const data = new Date;
var day = data.getDate();
var month = data.getMonth() + 1;
var year = data.getFullYear();

function listarTransacoes() {
    fetch(`http://localhost:5109/Finance/GetDate/${year}-${month}-${day}`, {
        method: 'GET',
        mode: 'cors' // Modo CORS
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
    .then(data => {
        const listarTransacoes = document.getElementById('listaTransacoes');
        listarTransacoes.innerHTML = '';
        data.forEach(element => {
            const ConteudoItem = document.createElement('div');
            ConteudoItem.innerHTML = `
            <p><strong>Tipo:</strong> ${element.tipo}</p>
            <p><strong>Data</strong> ${element.date}</p>
            <p><strong>Valor:</strong> R$ ${element.valor}</p>
            <p><strong>Descrição:</strong> ${element.descricao}</p>
            <p><strong>Categoria:</strong> ${element.categoria}</p>
            `;
            listarTransacoes.appendChild(ConteudoItem);
        });
        // Aqui você pode manipular os dados recebidos e exibi-los no seu HTML
    })
    .catch(error => {
        console.error('Erro ao listar transações:', error);
    });
}

// Chame a função para listar transações
listarTransacoes();
