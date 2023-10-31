import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  loginForm!: FormGroup;

  constructor(private fb: FormBuilder){ }

  ngOnInit(): void{
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit(){
    if (this.loginForm.valid){
      console.log(this.loginForm.value);
      //Enviar os dados a API
    }else{
      //Exibir uma mensagem erro
      this.validateAllFormFields(this.loginForm);
    }  
  
  }

  private validateAllFormFields(formGroup: FormGroup){
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if(control instanceof FormControl){
        control.markAsDirty({onlySelf:true})
      } else if (control instanceof FormGroup){
        this.validateAllFormFields(control)
      }
    });
  }

}
