﻿using BackEnd23Harkka.Models;
using BackEnd23Harkka.Repositories;

namespace BackEnd23Harkka.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;
        private readonly IUserRepository _userRepository;
        public MessageService(IMessageRepository repository, IUserRepository userRepository) {
            _repository= repository;
            _userRepository= userRepository;
        }

        public async Task<bool> DeleteMessageAsync(long id)
        {
            Message? message = await _repository.GetMessageAsync(id);
            if (message != null)
            {
                return await _repository.DeleteMessageAsync(message);
            } 
            return false;
        }

        public async Task<MessageDTO?> GetMessageAsync(long id)
        {
            
            return MessageToDto(await _repository.GetMessageAsync(id));
        }

        public async Task<IEnumerable<MessageDTO>> GetMessagesAsync()
        {
            IEnumerable<Message> messages = await _repository.GetMessagesAsync();
            List<MessageDTO> result = new List<MessageDTO>();
            foreach(Message message in messages)
            {
                result.Add(MessageToDto(message));
            }
            return result;

        }

        public async Task<MessageDTO> NewMessageAsync(MessageDTO message)
        {

            return MessageToDto(await _repository.NewMessageAsync(await DTOToMessage(message)));
        }

        public async Task<bool> UpdateMessageAsync(MessageDTO message)
        {
            Message? dbMessage = await _repository.GetMessageAsync(message.Id);
            if (dbMessage != null)
            {
                return await _repository.UpdateMessageAsync(await DTOToMessage(message));
            }

            return false;
        }

        private MessageDTO MessageToDto(Message message)
        {
            MessageDTO messageDTO = new MessageDTO();
            messageDTO.Id = message.Id;
            messageDTO.Title = message.Title;
            messageDTO.Body = message.Body;
            messageDTO.Sender = message.Sender.userName;
            if(message.Recipient!=null)
            {
                messageDTO.Recipient = message.Recipient.userName;
            }
            if(message.prevMessage!=null)
            {
                messageDTO.prevMessageID = message.prevMessage.Id;
            }

            return messageDTO;
        }

        private async Task<Message?> DTOToMessage(MessageDTO dto)
        {
            Message newMessage = new Message();

            newMessage.Id = dto.Id;
            newMessage.Title = dto.Title;
            newMessage.Body = dto.Body;

            User? sender = await _userRepository.GetUserAsync(dto.Sender);

            if (sender != null)
            {
                newMessage.Sender = sender;
            }
            if(dto.Recipient!= null)
            {
                User? recipient = await _userRepository.GetUserAsync(dto.Recipient);
                if (recipient == null)
                {
                    return null;
                }
                newMessage.Recipient = recipient;
            }
            if(dto.prevMessageID!=null && dto.prevMessageID>0) 
            {
                Message? prevMessage = await _repository.GetMessageAsync((long)dto.prevMessageID);
            }


            return newMessage;
        }
    }
}
