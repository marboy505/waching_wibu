package com.example.megumi.service.impl;

import com.example.megumi.model.Content;
import com.example.megumi.repository.ContentRepository;
import com.example.megumi.service.ContentService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class ContentServiceImpl implements ContentService {

    @Autowired
    private ContentRepository contentRepository;

    @Override
    public List<Content> getAllContent() {
        return contentRepository.findAll();
    }

    @Override
    public Optional<Content> getContentById(Long id) {
        return contentRepository.findById(id);
    }

    @Override
    public Content save(Content content) {
        return contentRepository.save(content);
    }

    @Override
    public void delete(Long id) {
        contentRepository.deleteById(id);
    }
}
