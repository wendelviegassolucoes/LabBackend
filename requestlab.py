import threading
import re
import requests
import random
from faker import Faker

def generate_cnpj():                                                       
    def calculate_special_digit(l):                                             
        digit = 0                                                               
                                                                                
        for i, v in enumerate(l):                                               
            digit += v * (i % 8 + 2)                                            
                                                                                
        digit = 11 - digit % 11                 
                                                                                
        return digit if digit < 10 else 0                                       
                                                                                
    cnpj =  [1, 0, 0, 0] + [random.randint(0, 9) for x in range(8)]             
                                                                                
    for _ in range(2):                                                          
        cnpj = [calculate_special_digit(cnpj)] + cnpj                           
                                                                                
    return '%s%s.%s%s%s.%s%s%s/%s%s%s%s-%s%s' % tuple(cnpj[::-1])

def send_post_request():
    # Endpoint para inserir empresas
    ENDPOINT = "http://localhost:9999/api/Empresa/Insert"

    # Gere um CNPJ válido
    cnpj_gerado = generate_cnpj()

    faker = Faker()

    # Gere nomes aleatórios para NomeFantasia e RazaoSocial
    nome_fantasia = faker.company()
    razao_social = faker.company_suffix()

    # Montar o payload JSON
    payload = {
        "Cnpj": cnpj_gerado,
        "NomeFantasia": nome_fantasia,
        "RazaoSocial": razao_social
    }

    contador = 0

    while contador < 15000:
        # Realizar a solicitação POST
        response = requests.post(ENDPOINT, json=payload)
        contador += 1

# Crie várias threads para executar a função send_post_request
num_threads = 32 # Número de threads que você deseja criar

threads = []
for _ in range(num_threads):
    thread = threading.Thread(target=send_post_request)
    threads.append(thread)
    thread.start()

# Aguarde todas as threads terminarem
for thread in threads:
    thread.join()

print("Todas as threads concluíram.")
