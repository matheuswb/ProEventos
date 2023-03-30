import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public eventos: any = [];
  public eventosFiltrados: any = [];
  larguraImagem: number = 150;
  margemImagem: number = 2;
  exibirImagem: boolean = true;
  private _filtroLista: string = '';

  public get filtroLista() {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    /*Se filtroLista tem valor eu chamo filtrar se não todos os eventos*/
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  filtrarEventos(filtrarPor: string): any {
    /*Para não se preocupar com o usuario digitar maiusculo ou minusculo*/
    filtrarPor = filtrarPor.toLocaleUpperCase();
    /*Uso o filtro do js - Dado evento eu pego e comparo com tema e local tudo minusculo*/
    return this.eventos.filter(
      (evento: any) => evento.tema.toLocaleUpperCase().indexOf(filtrarPor) !== -1 ||
                       evento.local.toLocaleUpperCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(private http: HttpClient) { }

  /* Método usado antes de chamar o HTML */
  ngOnInit() {
    this.getEventos();
  }

  alterarImagem() {
    this.exibirImagem = !this.exibirImagem;
  }

  public getEventos(): void {
    this.http.get('https://localhost:5001/api/Eventos').subscribe(
      /*É preciso alimentar eventosFiltrados juntamente com os eventos*/
      response => {
        this.eventos = response;
        this.eventosFiltrados = this.eventos;
      }, error => console.log(error)
    );
  }

}
