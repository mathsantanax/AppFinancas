document.getElementById('entradaForm').addEventListener('submit', function(event) {
    event.preventDefault();
    const valor = document.getElementById('valorEntrada').value;
    const descricao = document.getElementById('descricaoEntrada').value;
    const categoria = document.getElementById('categoriaEntrada').value;

    const entradaData = {
        valor: parseFloat(valor),
        descricao: descricao,
        tipo: "Entrada",
        categoria: categoria
    };

    fetch('http://localhost:5109/Financeiro/Entrada', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(entradaData)
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

document.getElementById('saidaForm').addEventListener('submit', function(event) {
    event.preventDefault();
    const valor = document.getElementById('valorSaida').value;
    const descricao = document.getElementById('descricaoSaida').value;
    const categoria = document.getElementById('categoriaSaida').value;

    const saidaData = {
        valor: parseFloat(valor),
        descricao: descricao,
        tipo: "Saida",
        categoria: categoria
    };

    fetch('http://localhost:5109/Financeiro/Saida', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(saidaData)
    })
    .then(response => response.json())
    .then(data => {
        alert('Saída registrada com sucesso!');
        listarTransacoes();
    })
    .catch(error => {
        console.error('Erro:', error);
    });
});

function listarTransacoes() {
    fetch('http://localhost:5109/Finance/GetDate/2024-10-09', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        },
        mode: 'cors' // Modo CORS
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
    .then(data => {
        console.log('Transações:', data);
        // Aqui você pode manipular os dados recebidos e exibi-los no seu HTML
    })
    .catch(error => {
        console.error('Erro ao listar transações:', error);
    });
}

// Chame a função para listar transações
listarTransacoes();
