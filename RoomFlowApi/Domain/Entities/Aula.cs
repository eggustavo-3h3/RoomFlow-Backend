﻿using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.Entities
{
    public class Aula
    {
        public Guid Id { get; set; }
        public EnumBloco Bloco { get; set; }
        public Guid DisciplinaId { get; set; }
        public Guid SalaId { get; set; }
        public Guid TurmaId { get; set; }
        public DateTime Data { get; set; }
        public Guid ProfessorId { get; set; }
        public Guid CursoId { get; set; }

        
        #region Propriedades de Navegabilidade

        public  Disciplina? Disciplina { get; set; } 
        public  Sala? Sala { get; set; }
        public  Turma? Turma { get; set; }
        public  Usuario? Usuario { get; set; }

        #endregion
    }
}     

        